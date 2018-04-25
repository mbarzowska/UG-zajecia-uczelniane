require_relative '../lib/acronym'

RSpec.describe '../lib/acronym.rb' do
  describe '#abbreviate' do
    it 'passes basic test, when string is quite simple' do
      expect(Acronym.abbreviate('Portable Network Graphics')).to eq 'PNG'
    end

    it 'passes when string contains lowercase starting word' do
      expect(Acronym.abbreviate('Ruby on Rails')).to eq 'ROR'
    end

    it 'passes when string contains punctation' do
      expect(Acronym.abbreviate('First In, First Out')).to eq 'FIFO'
    end

    it 'passes when string contains only uppercase starting word' do
      expect(Acronym.abbreviate('PHP: Hypertext Preprocessor')).to eq 'PHP'
    end

    it 'passes when string contains only uppercase starting word and one is an acronym' do
      expect(Acronym.abbreviate('GNU Image Manipulation Program')).to eq 'GIMP'
    end

    it 'passes when string contains -' do
      expect(Acronym.abbreviate('Complementary metal-oxide semiconductor')).to eq 'CMOS'
    end
  end
end
