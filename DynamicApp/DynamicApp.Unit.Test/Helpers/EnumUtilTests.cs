using Application.Models;
using Application.Helpers;
using System.ComponentModel;

namespace DynamicApp.Unit.Test.Validators
{
    public class EnumUtilTests
    {
        public enum TestEnum
        {
            [Description("This is Value1")]
            Value1,
            [Description("This is Value2")]
            Value2,
            Value3
        }

        [Fact]
        public void GetEnum_ShouldReturnCorrectNumberOfItems()
        {
            // Arrange
            var expectedCount = Enum.GetValues(typeof(TestEnum)).Length;

            // Act
            var result = EnumUtil.GetEnum<TestEnum>();

            // Assert
            Assert.Equal(expectedCount, result.Count);
        }

        [Fact]
        public void GetEnum_ShouldReturnCorrectEnumValues()
        {
            // Arrange
            var expectedValues = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>().ToList();

            // Act
            var result = EnumUtil.GetEnum<TestEnum>();

            // Assert
            Assert.Equal(expectedValues, result.Select(x => (TestEnum)x.Value));
        }

        [Fact]
        public void GetEnum_ShouldReturnEnumResponseModelInstances()
        {
            // Act
            var result = EnumUtil.GetEnum<TestEnum>();

            // Assert
            Assert.All(result, x => Assert.IsType<EnumResponseModel>(x));
        }
        [Fact]
        public void GetDescription_ShouldReturnDescription()
        {
            // Arrange
            TestEnum enumValueWithDescription = TestEnum.Value1;
            TestEnum enumValueWithoutDescription = TestEnum.Value3;

            // Act
            string description1 = enumValueWithDescription.GetDescription();
            string description2 = enumValueWithoutDescription.GetDescription();

            // Assert
            Assert.Equal("This is Value1", description1);
            Assert.Equal("Value3", description2); // Value3 does not have a Description attribute
        }
    }

}