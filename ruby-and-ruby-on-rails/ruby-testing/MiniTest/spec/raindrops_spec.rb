require_relative '../lib/raindrops'

RSpec.describe '../lib/raindrops.rb' do
  describe '#pangram?' do
    it 'tests_the_sound_for_1_is_1' do
      expect(Raindrops.convert(1)).to eq '1'
    end

    it 'tests_the_sound_for_3_is_pling' do
      expect(Raindrops.convert(3)).to eq 'Pling'
    end

    it 'tests_the_sound_for_5_is_plang' do
      expect(Raindrops.convert(5)).to eq 'Plang'
    end

    it 'tests_the_sound_for_7_is_plong' do
      expect(Raindrops.convert(7)).to eq 'Plong'
    end

    it 'tests_the_sound_for_6_is_pling_as_it_has_a_factor_3' do
      expect(Raindrops.convert(6)).to eq 'Pling'
    end

    it 'tests_2_to_the_power_3_does_not_make_a_raindrop_sound_as_3_is_the_exponent_not_the_base' do
      expect(Raindrops.convert(8)).to eq '8'
    end

    it 'tests_the_sound_for_9_is_pling_as_it_has_a_factor_3' do
      expect(Raindrops.convert(9)).to eq 'Pling'
    end

    it 'tests_the_sound_for_10_is_plang_as_it_has_a_factor_5' do
      expect(Raindrops.convert(10)).to eq 'Plang'
    end

    it 'tests_the_sound_for_14_is_plong_as_it_has_a_factor_of_7' do
      expect(Raindrops.convert(14)).to eq 'Plong'
    end

    it 'tests_the_sound_for_15_is_plingplang_as_it_has_factors_3_and_5' do
      expect(Raindrops.convert(15)).to eq 'PlingPlang'
    end

    it 'tests_the_sound_for_21_is_plingplong_as_it_has_factors_3_and_7' do
      expect(Raindrops.convert(21)).to eq 'PlingPlong'
    end

    it 'tests_the_sound_for_25_is_plang_as_it_has_a_factor_5' do
      expect(Raindrops.convert(25)).to eq 'Plang'
    end

    it 'tests_the_sound_for_27_is_pling_as_it_has_a_factor_3' do
      expect(Raindrops.convert(27)).to eq 'Pling'
    end

    it 'tests_the_sound_for_35_is_plangplong_as_it_has_factors_5_and_7' do
      expect(Raindrops.convert(35)).to eq 'PlangPlong'
    end

    it 'tests_the_sound_for_49_is_plong_as_it_has_a_factor_7' do
      expect(Raindrops.convert(49)).to eq 'Plong'
    end

    it 'tests_the_sound_for_52_is_52' do
      expect(Raindrops.convert(52)).to eq '52'
    end

    it 'tests_the_sound_for_105_is_plingplangplong_as_it_has_factors_3_5_and_7' do
      expect(Raindrops.convert(105)).to eq 'PlingPlangPlong'
    end

    it 'tests_the_sound_for_3125_is_plang_as_it_has_a_factor_5' do
      expect(Raindrops.convert(3125)).to eq 'Plang'
    end
  end
end
