using System;
using System.Collections.Generic;
using System.Linq;
using FakeUpLib.Fluent;
using FluentAssertions;
using Xunit;

namespace FakeUp.Tests.Examples
{
    public class ExampleTests
    {
        [Fact]
        public void FillingValuesBasedOnRelativePaths()
        {
            var result = FakeUpLib.FakeUp.NewObject<Foo>(o => o
                .FillAll<string>().With("default")
                .Fill((Bar bar) => bar.Baz.Value).With("bar baz")
            );
            
            result.ShouldBeEquivalentTo(new Foo
            {
                Bar = new Bar
                {
                    Baz = new Baz { Value = "bar baz" }
                },
                Baz = new Baz { Value = "default" },
                Value = "default"
            });
        }

        [Fact]
        public void ReadmeExample_Works()
        {
            // Act
            var result = FakeUpLib.FakeUp.NewObject<ReceiptBook>(o => o
                .WithCollectionsSize(3)
                .AddState(() => new FoodReceiptState())
                
                .FillAll<string>().WithGuid()

                .FillAll<Ingredient>()
                .With(ctx => ctx.GetState<FoodReceiptState>().GetIngredient())

                .Fill(book => book.Ingredients)
                .With(ctx => ctx.GetState<FoodReceiptState>().AllIngredients)
            );
            
            //Assert
            result.Receipts.SelectMany(r => r.Ingredients).Distinct().Should().BeEquivalentTo(result.Ingredients);
        }

        class FoodReceiptState
        {
            readonly Random random = new Random();
            
            public List<Ingredient> AllIngredients { get; } = new List<Ingredient>();

            public Ingredient GetIngredient()
            {
                if (!this.AllIngredients.Any() || this.random.NextDouble() > 0.5)
                {
                    var ingredient = NewIngredient();
                    this.AllIngredients.Add(ingredient);
                    return ingredient;
                }

                return this.AllIngredients[this.random.Next(0, this.AllIngredients.Count)];
            }

            private Ingredient NewIngredient() => FakeUpLib.FakeUp.NewObject<Ingredient>(o => o.FillAll<string>().WithGuid());
        }

        class ReceiptBook
        {
            public List<Ingredient> Ingredients { get; set; }
            public List<FoodReceipt> Receipts { get; set; }
        }

        class FoodReceipt
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public List<Ingredient> Ingredients { get; set; }
        }

        class Ingredient
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }

    class Foo
    {
        public Bar Bar { get; set; }
        public Baz Baz { get; set; }
        public string Value { get; set; }
    }

    class Bar
    {
        public Baz Baz { get; set; }
    }

    class Baz
    {
        public string Value { get; set; }
    }
}