using Xunit;
using System.Collections.Generic;
using Zigma.Models;
using Zigma.TransformationTools;
using System.ComponentModel;

namespace Zigma.Tests
{
    public class StructureTransformTests
    {
        [Fact]
        public void ColumnRemove_ShouldRemoveGivenColumn()
        {
            // Arrange
            List<string[]> TestSet = new List<string[]>{
                new string[] {"aa", "bb", "cc", "dd"},
                new string[] {"ee", "ff", "gg", "hh"},
                new string[] {"ii", "jj", "kk", "ll"}
            };

            List<string[]> ExpectedTestSet = new List<string[]>{
                new string[] {"aa", "cc", "dd"},
                new string[] {"ee", "gg", "hh"},
                new string[] {"ii", "kk", "ll"}
            }; 

            ZigmaModel expectedModel = new();
            expectedModel.CreateZigmaDatasetFromRawDataset(ExpectedTestSet);

            // Act
            ZigmaModel actualModel = new();
            actualModel.CreateZigmaDatasetFromRawDataset(TestSet);
            TransformationTool transform = new();
            actualModel = transform.ColumnRemove(actualModel, 1);

            // Assert
            Assert.Equal(expectedModel.GetRawZigmaDataset(), actualModel.GetRawZigmaDataset());

        }
    }
}
