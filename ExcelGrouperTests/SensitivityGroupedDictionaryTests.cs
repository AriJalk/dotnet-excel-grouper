using ExcelGrouper.DataStructures;

namespace Tests
{
	public class SensitivityGroupedDictionaryTests
	{
		[Fact]
		public void WhenValuesAreInRange_ItReturnsTheSameGroup()
		{
			//Arrange
			int sensitivity = 2;
			List<float> group1 = [1, 1, 1, 1];
			List<float> group2 = [1, 1, 1, 3];
			List<float> group3 = [1, 1, 2, 3];
			List<float> group4 = [-1, -1, 2, 3];
			SensitivityGroupedDictionary dictionary = new SensitivityGroupedDictionary(sensitivity);

			//Act
			HashSet<int> groupIdSets = new HashSet<int>() { dictionary.GetGroupId(group1), dictionary.GetGroupId(group2), dictionary.GetGroupId(group3), dictionary.GetGroupId(group4) };

			//Assert
			Assert.Single(groupIdSets);
		}

		[Fact]
		public void WhenValuesAreNotInRange_ItReturnsDifferentGroups()
		{
			//Arrange
			int sensitiviy = 0;
			List<float> group1 = [1, 1, 1, 1];
			List<float> group2 = [1, 1, 1, 3];
			List<float> group3 = [1, 1, 2, 3];
			List<float> group4 = [-1, -1, 2, 3];
			SensitivityGroupedDictionary dictionary = new SensitivityGroupedDictionary(sensitiviy);

			//Act
			HashSet<int> groupIdSets = new HashSet<int>() { dictionary.GetGroupId(group1), dictionary.GetGroupId(group2), dictionary.GetGroupId(group3), dictionary.GetGroupId(group4) };

			//Assert
			Assert.Equal(4, groupIdSets.Count);
		}

		//[Fact]
		//public void WhenValuesAreWithinThreeDigitPrecision_ItReturnsSameGroup()
		//{
		//	// Arrange
		//	float sensitivity = 0.001f;
		//	List<float> group1 = new List<float> { 1.000f, 1.000f, 1.000f, 1.000f };
		//	List<float> group2 = new List<float> { 1.000f, 1.000f, 1.001f, 1.001f };
		//	List<float> group3 = new List<float> { 1.000f, 1.000f, 1.005f, 1.005f };
		//	List<float> group4 = new List<float> { 3.000f, 3.000f, 3.000f, 3.000f }; // Out of range

		//	SensitivityGroupedDictionary dictionary = new SensitivityGroupedDictionary(sensitivity);

		//	// Act
		//	HashSet<int> groupIdSets = new HashSet<int>()
		//	{
		//		dictionary.GetGroupId(group1),
		//		dictionary.GetGroupId(group2),
		//		dictionary.GetGroupId(group3),
		//		dictionary.GetGroupId(group4)
		//	};

		//	// Assert
		//	// group1, group2, and group3 should be in the same group because their values are within the sensitivity range
		//	// group4 should be in a separate group due to the large difference in values
		//	Assert.Equal(2, groupIdSets.Count);
		//}

	}
}
