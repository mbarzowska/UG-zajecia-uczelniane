require_relative "../lib/01_16"

RSpec.describe "../lib/01_16.rb" do
  describe "#insert_multiple" do
    it "inserts multiple strings" do
      input = { 3=>"<b>", 6=>"</b>" }
      str = "aaabbbccc"
      expected = "aaa<b>bbb</b>ccc"
      expect(str.insert_multiple(input)).to eq expected
    end
  end
end
