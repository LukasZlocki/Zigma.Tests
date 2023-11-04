using Xunit;
using System.Collections.Generic;
using Zigma.Models;
using Zigma.TransformationTools;

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

        [Fact]
        public void TransformColumnToDate_ShouldTransformToProperDate()
        {
            // Arrange
            List<string[]> TestSet = new List<string[]>{
                new string[] {"aa", "2022-02-21 14:52:48" , "cc", "dd"},
                new string[] {"ee", "2022-02-21 00:00:00", "gg", "hh"},
                new string[] {"ii", "2022-02-21 14:52:48", "kk", "ll"}
            };

            List<string[]> ExpectedSet = new List<string[]>
            {
                new string[] {"aa", "2022-02-21" , "cc", "dd"},
                new string[] {"ee", "2022-02-21", "gg", "hh"},
                new string[] {"ii", "2022-02-21", "kk", "ll"}
            };
            ZigmaModel expectedModel = new();
            expectedModel.CreateZigmaDatasetFromRawDataset(ExpectedSet);

            // Act
            ZigmaModel actualModel = new();
            TransformationTool transform = new();
            actualModel.CreateZigmaDatasetFromRawDataset(TestSet);
            actualModel = transform.TransformColumnToDate(actualModel, 1);

            // Assert
            Assert.Equal(expectedModel.GetRawZigmaDataset(), actualModel.GetRawZigmaDataset());
        }

        [Fact]
        public void ExtractColumnToNewZigmaModel_ShouldReturnExttractedColumnAsZigmaModel()
        {
            // Arrange
            List<string[]> TestSet = new List<string[]>{
                new string[] {"aa", "Date" , "cc", "dd"},
                new string[] {"ee", "2022-02-21", "gg", "hh"},
                new string[] {"ii", "2022-02-21", "kk", "ll"}
            };

            List<string[]> ExpectedSet = new List<string[]>
            {
                new string[] {"Date"},
                new string[] {"2022-02-21"},
                new string[] {"2022-02-21"}
            };
            ZigmaModel expectedModel = new();
            expectedModel.CreateZigmaDatasetFromRawDataset(ExpectedSet);

            // Act
            ZigmaModel actualModel = new();
            TransformationTool transform = new();
            actualModel.CreateZigmaDatasetFromRawDataset(TestSet);
            actualModel = transform.ColumnExtract(actualModel, 1);

            // Assert
            Assert.Equal(expectedModel.GetRawZigmaDataset(), actualModel.GetRawZigmaDataset());
        }

         [Fact]
        public void RemoveRecurrenceData_ShouldReturnRemovedRecurrenceDataAsZigmaModel()
        {
            // Arrange
            List<string[]> TestSet = new List<string[]>{
                new string[] {"aa", "Date" , "cc", "dd"},
                new string[] {"ee", "2022-02-21", "gg", "hh"},
                new string[] {"ee", "2022-02-21", "gg", "hh"},
                new string[] {"ii", "2022-02-22", "kk", "ll"},
                new string[] {"ii", "2022-02-23", "kk", "ll"},
                new string[] {"ii", "2022-02-24", "kk", "ll"},
                new string[] {"ii", "2022-02-22", "kk", "ll"}
            };

            List<string[]> ExpectedSet = new List<string[]>
            {
                new string[] {"aa", "Date" , "cc", "dd"},
                new string[] {"ee", "2022-02-21", "gg", "hh"},
                new string[] {"ii", "2022-02-22", "kk", "ll"},
                new string[] {"ii", "2022-02-23", "kk", "ll"},
                new string[] {"ii", "2022-02-24", "kk", "ll"}
            };
            ZigmaModel expectedModel = new();
            expectedModel.CreateZigmaDatasetFromRawDataset(ExpectedSet);

            // Act
            ZigmaModel actualModel = new();
            TransformationTool transform = new();
            actualModel.CreateZigmaDatasetFromRawDataset(TestSet);
            actualModel = transform.RemoveRecurrenceData(actualModel, 1);

            // Assert
            Assert.Equal(expectedModel.GetRawZigmaDataset(), actualModel.GetRawZigmaDataset());
        }


    }
}
