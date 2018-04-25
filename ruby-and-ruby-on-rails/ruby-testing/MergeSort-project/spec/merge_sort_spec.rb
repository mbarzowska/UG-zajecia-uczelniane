require 'merge_sort'

describe Merge do
  describe "#raises errors due to invalid input type" do
    it 'raises an ArgumentError, when supposed array is a string' do
      supposed_array = 'QWERTY'
      expect { Merge.merge_sort(supposed_array) }.to raise_error(ArgumentError)
    end
    it 'raises an ArgumentError, when supposed array is an integer' do
      supposed_array = 10
      expect { Merge.merge_sort(supposed_array) }.to raise_error(ArgumentError)
    end
    it 'raises an ArgumentError, when supposed array is a floating point number' do
      supposed_array = 10.10
      expect { Merge.merge_sort(supposed_array) }.to raise_error(ArgumentError)
    end
  end
  describe "#tests for single values" do
    it 'returns empty when given array is empty' do
      array = []
      expect(Merge.merge_sort(array)).to be_empty
    end
    it 'returns a single number when array has only one int element' do
      array = [10]
      expect(Merge.merge_sort(array)).to eq [10]
    end
    it 'returns a single number when array has only one float element' do
      array = [10.10]
      expect(Merge.merge_sort(array)).to eq [10.10]
    end
    it 'returns a single character when array has only one char element' do
      array = [10.10]
      expect(Merge.merge_sort(array)).to eq [10.10]
    end
  end
  describe "#tests for random order of given input" do
    it 'sorts an array given with int numbers in random order' do
      array = [7, 6, 5, 9, 8, 4, 3, 1, 2, 0]
      array2 = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
      expect(Merge.merge_sort(array)).to eq array2
    end
    it 'sorts an array given with float numbers in random order' do
      array = [7.4, 6.0, 5.5, 9.3, 8.73, 5.4, 8.72, 1.67, 2.0, 0.9]
      array2 = [0.9, 1.67, 2.0, 5.4, 5.5, 6.0, 7.4, 8.72, 8.73, 9.3]
      expect(Merge.merge_sort(array)).to eq array2
    end
    it 'sorts an array given with mixed type numbers in random order' do
      array = [7.4, 6.0, 5.5, 9, 8.73, 5.4, 8.72, 1, 2.0, 0.9]
      array2 = [0.9, 1, 2.0, 5.4, 5.5, 6.0, 7.4, 8.72, 8.73, 9]
      expect(Merge.merge_sort(array)).to eq array2
    end
    it 'sorts an array given with characters in random order' do
      array = ['B', 'd', ' ', 'a', 'c', 'C', 'A', 'D', 'b']
      array2 = [' ', 'A', 'B', 'C', 'D', 'a', 'b', 'c', 'd']
      expect(Merge.merge_sort(array)).to eq array2
    end
    it 'sorts an array given with strings in random order' do
      array = %w[And the rest is rust and stardust]
      array2 = %w[And and is rest rust stardust the]
      expect(Merge.merge_sort(array)).to eq array2
    end
  end
  describe "#tests for another qualification of input type" do
    # assuming if the above passed, there's no need to repeat these tests for every type of data
    it 'sorts an array given with negative int numbers in random order' do
      array = [-19, -4, -16, -12]
      expect(Merge.merge_sort(array)).to eq [-19, -16, -12, -4]
    end
    it 'sorts an array given with positive and negative int numbers in random order' do
      array = [-19, 4, -16, 12]
      expect(Merge.merge_sort(array)).to eq [-19, -16, 4, 12]
    end
    it 'sorts (leaves unbroken) an array given with int numbers in ascending order' do
      array = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
      array2 = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
      expect(Merge.merge_sort(array)).to eq array2
    end
    it 'sorts an array given with int numbers in descending order' do
      array = [9, 8, 7, 6, 5, 4, 3, 2, 1, 0]
      array2 = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
      expect(Merge.merge_sort(array)).to eq array2
    end
  end
  describe "#tests for other than main functionalities" do
    it 'checks if a is smaller than b when a really is smaller than b' do
      a = 1
      b = 10
      expect(Merge.less?(a, b)).to eq true
    end
    it 'checks if a is smaller than b when a really is bigger than b' do
      a = 10
      b = 1
      expect(Merge.less?(a, b)).to eq false
    end
    it 'checks if array is sorted with iterative usage of less in method sorted? when it is' do
      array = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
      expect(Merge.sorted?(array)).to eq true
    end
    it 'checks if array is sorted with iterative usage of less in method sorted? when it isnt' do
      array = [10, 1, 2, 3, 4, 5, 6, 7, 8, 9]
      expect(Merge.sorted?(array)).to eq false
    end
  end
end
