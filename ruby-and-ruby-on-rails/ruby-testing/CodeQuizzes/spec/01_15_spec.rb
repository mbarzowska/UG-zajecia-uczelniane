require_relative "../lib/01_15"

RSpec.describe "../lib/01_15.rb" do
  describe "#concat" do
    it "concatenates two integers" do
      expect(42.concat(99)).to eq 4299
    end
  end
end
