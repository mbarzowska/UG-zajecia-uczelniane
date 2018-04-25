require_relative "../lib/01_14"

RSpec.describe "../lib/01_14.rb" do
  describe "#numeric?" do
    it "returns true if string can be converted to Float" do
      expect("12.48".numeric?).to be true
    end

    it "returns false if string cannot be converted to Float" do
      expect("blah".numeric?).to be false
    end
  end
end
