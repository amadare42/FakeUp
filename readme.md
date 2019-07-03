# Overview

*FakeUp* is small project that allows to generate fake data objects with complex structure for tests.
The goal is to create API that is concise and modular, so creating fixtures for nested and/or dependent structures is painless or at least a lot less painful.
It is just worse version of [AutoFixture](https://github.com/AutoFixture/AutoFixture) but some tasks that I was needed can be done more easily with *FakeUp* since it have to narrower scope and more specialized features.

## Features highlight

* [Filling values based on relative paths](#filling-values-based-on-relative-paths)
* [Sharing state](#sharing-state)

### Filling values based on relative paths

For use when you have reoccurring structures with some special cases.
 ```c#
 class Foo {
    Bar Bar { get; }    
    Baz Baz { get; }
    string Value { get; }
 }
 class Bar {
    Baz Baz { get; }
 }
 class Baz {
    string Value { get; }
 }
 ```
 When creating fixture with following code
 ```c#
 var result = FakeUp.NewObject<Foo>(o => o
    .FillAll<string>().With("default")
    .Fill((Bar bar) => bar.Baz.Value).With("bar baz")
 )
 ```
 You will receive the following:
 ```
 Foo
    Bar.Baz.Value: "bar baz"
    Baz.Value: "default"
    Value: "default"
 ```

### Sharing state

When you have values that are dependent on one another, you can use `State` to manage shared values.
For example we have following structure:
```c#
class ReceiptBook {
    public List<Ingredient> Ingredients { get; set; }
    public List<FoodReceipt> Receipts { get; set; }
}
class FoodReceipt {
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }
}
class Ingredient {
    public string Name { get; set; }
}
```
We want `ReceiptBook.Ingredients` to contain all ingredients used in all `FoodReceipts`. We can achieve this using object that will track all added ingredients:

```c#
class FoodReceiptState
{    
    public List<Ingredient> AllIngredients { get; } = new List<Ingredient>();

    public Ingredient GetIngredient()
    {
        if (!this.AllIngredients.Any() || new Random().NextDouble() > 0.5)
        {
            var ingredient = NewIngredient();
            this.AllIngredients.Add(ingredient);
            return ingredient;
        }

        return this.AllIngredients[this.Random.Next(0, this.AllIngredients.Count)];
    }

    private Ingredient NewIngredient() => FakeUp.NewObject<Ingredient>(o => o.FillAll<string>().WithGuid());
}
```
The idea is, that every time `FakeUp` will require `FoodReceipt.Ingredient` we will return either new `Ingredient` and add it to list or return excising one. So `AllIngredients` will always contain all generated ingredients.

Now following declaration will create object with required structure:
```c#
FakeUp.NewObject<ReceiptBook>(o => o
    .WithCollectionsSize(3)
    .AddState(() => new FoodReceiptState())
    
    .FillAll<string>().WithGuid()

    .FillAll<Ingredient>()
    .With(ctx => ctx.GetState<FoodReceiptState>().GetIngredient())

    .Fill(book => book.Ingredients)
    .With(ctx => ctx.GetState<FoodReceiptState>().AllIngredients)
);
```

