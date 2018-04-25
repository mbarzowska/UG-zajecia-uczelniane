require_relative "../lib/02_06"

RSpec.describe "../lib/02_06.rb" do
  describe "#super_compact" do
    it "removes nil and empty elements" do
      arr = [:bob, "", nil, [], "joe"]
      expected = [:bob, "joe"]
      expect(arr.super_compact).to eq expected
    end
  end
end
