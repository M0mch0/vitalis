using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitalis.Data.Models;
using Vitalis.Data.Repository.Contracts;
using Vitalis.Services.Core;
using Vitalis.Services.Core.Contracts;

namespace Vitalis.Services.Tests
{
    public class CatalogServiceTests
    {
        private ICatalogService catalogService;

        [SetUp]
        public void Setup()
        {
            // Arrange  
            Mock<IMealRepository> mealRepoMock = new Mock<IMealRepository>();
            mealRepoMock.Setup(r => r.GetAllMealsAsync()).ReturnsAsync(new List<Meal>
            {
                new Meal { Id = 1, Name = "Spaghetti Bolognese"/*, Ingredients = new List<MealIngredient>() {
                                                                                    new MealIngredient { MealId = 1, IngredientId = 3 }
                                                                                   ,new MealIngredient{ MealId = 1, IngredientId = 4} }
                                                               , Tags = new List<MealTag>() {
                                                                              new MealTag { MealId = 1, TagId = 1 }
                                                                             ,new MealTag{ MealId = 1, TagId = 2} }*/},

                new Meal { Id = 2, Name = "Chicken Alfredo"/*, Ingredients = new List<MealIngredient>() {
                                                                                 new MealIngredient { MealId = 1, IngredientId = 3 }
                                                                                ,new MealIngredient{ MealId = 1, IngredientId = 5} }
                                                           , Tags = new List<MealTag>() {
                                                                              new MealTag { MealId = 2, TagId = 2 }
                                                                             ,new MealTag{ MealId = 2, TagId = 4} }*/},

                new Meal { Id = 3, Name = "Vegetable Stir Fry"/*, Ingredients = new List<MealIngredient>() {
                                                                              new MealIngredient { MealId = 1, IngredientId = 2 }
                                                                             ,new MealIngredient{ MealId = 1, IngredientId = 6} }
                                                              , Tags = new List<MealTag>() {
                                                                              new MealTag { MealId = 3, TagId = 3 }}*/},

                new Meal { Id = 4, Name = "Grilled Salmon with Asparagus"/*, Ingredients = new List<MealIngredient>() {
                                                                              new MealIngredient { MealId = 4, IngredientId = 1 }
                                                                             ,new MealIngredient{ MealId = 4, IngredientId = 2} }
                                                                         , Tags = new List<MealTag>() {
                                                                              new MealTag { MealId = 4, TagId = 3}
                                                                             ,new MealTag{ MealId = 4, TagId = 2} }*/}
            });

            Mock<IIngRepository> ingRepoMock = new Mock<IIngRepository>();
            ingRepoMock.Setup(r => r.GetAllIngredientsAsync()).ReturnsAsync(new List<Ingredient>
            {
                new Ingredient { Id = 1, Name = "Salmon"/*, Tags = new List<IngredientTag>() {
                                                                              new IngredientTag { IngredientId = 1, TagId = 3}
                                                                             ,new IngredientTag{ IngredientId = 1, TagId = 2} }
                                                             , NutrientProfile = new NutrientProfile()*/},
                new Ingredient { Id = 2, Name = "Asparagus"/*, Tags = new List<IngredientTag>() {
                                                                              new IngredientTag { IngredientId = 1, TagId = 3}
                                                                             ,new IngredientTag{ IngredientId = 1, TagId = 2} }
                                                             , NutrientProfile = new NutrientProfile()*/},
                new Ingredient { Id = 3, Name = "Chicken"/*, Tags = new List<IngredientTag>() {
                                                                              new IngredientTag { IngredientId = 1, TagId = 3}
                                                                             ,new IngredientTag{ IngredientId = 1, TagId = 2} }
                                                             , NutrientProfile = new NutrientProfile()*/},
                new Ingredient { Id = 4, Name = "Pasta"/*, Tags = new List<IngredientTag>() {
                                                                              new IngredientTag { IngredientId = 1, TagId = 3}
                                                                             ,new IngredientTag{ IngredientId = 1, TagId = 2} }
                                                             , NutrientProfile = new NutrientProfile()*/},
                new Ingredient { Id = 5, Name = "Alfredo Sauce"/*, Tags = new List<IngredientTag>() {
                                                                              new IngredientTag { IngredientId = 1, TagId = 3}
                                                                             ,new IngredientTag{ IngredientId = 1, TagId = 2} }
                                                             , NutrientProfile = new NutrientProfile()*/},
                new Ingredient { Id = 6, Name = "Vegetables"/*, Tags = new List<IngredientTag>() {
                                                                              new IngredientTag { IngredientId = 1, TagId = 3}
                                                                             ,new IngredientTag{ IngredientId = 1, TagId = 2} }
                                                             , NutrientProfile = new NutrientProfile()*/}
            });

            Mock<ITagRepository> tagRepositoryMock = new Mock<ITagRepository>();
            tagRepositoryMock.Setup(r => r.GetAllTagsAsync()).ReturnsAsync(new List<Tag>
            {
                new Tag { Id = 1, Name = "Carbs" },
                new Tag { Id = 2, Name = "Meat" },
                new Tag { Id = 3, Name = "Vegetables" },
                new Tag { Id = 4, Name = "Protein" }
            });
            catalogService = new CatalogService(mealRepoMock.Object, tagRepositoryMock.Object, ingRepoMock.Object);
        }


        [Test]
        public async Task IsGetAllMeals_SearchQueryForName_ReturnsCorrectMeals()
        {
            //Arrange
            var searchQuery = "With";
            // Act
            var result = await catalogService.GetAllMealsAsync(searchQuery);
            // Assert
            Assert.That(result.Meals.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task IsGetAllMeals_SearchQueryForName_ReturnsNothing()
        {
            //Arrange
            var searchQuery = "Poultry";
            // Act
            var result = await catalogService.GetAllMealsAsync(searchQuery);
            // Assert
            Assert.That(result.Meals.Count(), Is.EqualTo(0));
        }
        [Test]
        public async Task IsGetAllIngredients_SearchQueryForName_ReturnsCorrectIngs()
        {
            //Arrange
            var searchQuery = "a";
            // Act
            var result = await catalogService.GetAllIngredientsAsync(searchQuery);
            // Assert
            Assert.That(result.Ingredients.Count(), Is.EqualTo(2));
        }
    }
}
