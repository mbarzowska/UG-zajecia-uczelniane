require_relative "../lib/01_09"

RSpec.describe "../lib/01_09.rb" do
  describe "#date_convertible?" do
    it "returns true if can be converted to date" do
      str = "20120120"
      expect(str.date_convertible?).to be true
    end

    it "returns false if self cannot be converted to a date" do
      str = "blah"
      expect(str.date_convertible?).to be false
    end
  end
end
