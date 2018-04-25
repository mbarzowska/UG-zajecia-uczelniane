require_relative "../lib/01_07"

RSpec.describe "../lib/01_07.rb" do
  describe "#titlecase" do
    it "capitalizes the first letter of every word" do
      str = "the grand Gala party"
      expected = "The Grand Gala Party"
      expect(str.titlecase).to eq expected
    end
  end
end
