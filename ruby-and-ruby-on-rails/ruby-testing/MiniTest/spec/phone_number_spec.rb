require_relative '../lib/phone_number'

RSpec.describe '../lib/phone_number.rb' do
  describe '#clean' do
    it 'tests_cleans_the_number' do
      expect(PhoneNumber.clean('(223) 456-7890')).to eq '2234567890'
    end

    it 'tests_cleans_numbers_with_dots' do
      expect(PhoneNumber.clean('223.456.7890')).to eq '2234567890'
    end

    it 'tests_cleans_numbers_with_multiple_spaces' do
      expect(PhoneNumber.clean('223 456   7890   ')).to eq '2234567890'
    end

    it 'tests_invalid_when_9_digits' do
      expect(PhoneNumber.clean('123456789')).to eq nil
    end

    it 'tests_invalid_when_11_digits_does_not_start_with_a_1' do
      expect(PhoneNumber.clean('22234567890')).to eq nil
    end

    it 'tests_valid_when_11_digits_and_starting_with_1' do
      expect(PhoneNumber.clean('12234567890')).to eq '2234567890'
    end

    it 'tests_valid_when_11_digits_and_starting_with_1_even_with_punctuation' do
      expect(PhoneNumber.clean('+1 (223) 456-7890')).to eq '2234567890'
    end

    it 'tests_invalid_when_more_than_11_digits' do
      expect(PhoneNumber.clean('321234567890')).to eq nil
    end

    it 'tests_invalid_with_letters' do
      expect(PhoneNumber.clean('123-abc-7890')).to eq nil
    end

    it 'tests_invalid_with_punctuations' do
      expect(PhoneNumber.clean('123-@:!-7890')).to eq nil
    end

    it 'tests_invalid_if_area_code_does_not_start_with_2_9' do
      expect(PhoneNumber.clean('(123) 456-7890')).to eq nil
    end

    it 'tests_invalid_if_exchange_code_does_not_start_with_2_9' do
      expect(PhoneNumber.clean('(223) 056-7890')).to eq nil
    end
  end
end
