require_relative "../lib/01_04"

RSpec.describe "../lib/01_04.rb" do
  describe "#vowels" do
    it "returns array of words with vowels" do
      str = "24 hour roadside resistance"
      expected = %w|hour roadside resistance|
      expect(vowels(str)).to eq expected
    end
  end
end
