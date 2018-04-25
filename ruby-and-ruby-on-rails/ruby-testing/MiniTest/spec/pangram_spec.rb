require_relative '../lib/pangram'

RSpec.describe '../lib/pangram.rb' do
  describe '#pangram?' do
    it 'tests_sentence_empty' do
      expect(Pangram.pangram?('')).to eq false
    end

    it 'tests_pangram_with_only_lower_case' do
      expect(Pangram.pangram?('the quick brown fox jumps over the lazy dog')).to eq true
    end

    it 'tests_missing_character_x' do
      expect(Pangram.pangram?('a quick movement of the enemy will jeopardize five gunboats')).to eq false
    end

    it 'tests_another_missing_character_x' do
      expect(Pangram.pangram?('the quick brown fish jumps over the lazy dog')).to eq false
    end

    it 'tests_pangram_with_underscores' do
      expect(Pangram.pangram?('the_quick_brown_fox_jumps_over_the_lazy_dog')).to eq true
    end

    it 'tests_pangram_with_numbers' do
      expect(Pangram.pangram?('the 1 quick brown fox jumps over the 2 lazy dogs')).to eq true
    end

    it 'tests_missing_letters_replaced_by_numbers' do
      expect(Pangram.pangram?('7h3 qu1ck brown fox jumps ov3r 7h3 lazy dog')).to eq false
    end

    it 'tests_pangram_with_mixed_case_and_punctuation' do
      expect(Pangram.pangram?('"Five quacking Zephyrs jolt my wax bed."')).to eq true
    end

    it 'tests_upper_and_lower_case_versions_of_the_same_character_should_not_be_counted_separately' do
      expect(Pangram.pangram?('the quick brown fox jumps over with lazy FX')).to eq false
    end
  end
end
