require_relative '../lib/rna_transcription'

RSpec.describe '../lib/rna_transcription.rb' do
  describe '#of_dna' do
    it 'tests_rna_complement_of_cytosine_is_guanine' do
      expect(Complement.of_dna('C')).to eq 'G'
    end

    it 'tests_rna_complement_of_guanine_is_cytosine' do
      expect(Complement.of_dna('G')).to eq 'C'
    end

    it 'tests_rna_complement_of_thymine_is_adenine' do
      expect(Complement.of_dna('T')).to eq 'A'
    end

    it 'tests_rna_complement_of_adenine_is_uracil' do
      expect(Complement.of_dna('A')).to eq 'U'
    end

    it 'tests_rna_complement' do
      expect(Complement.of_dna('ACGTGGTCTTAA')).to eq 'UGCACCAGAAUU'
    end

    it 'tests_correctly_handles_invalid_input_rna_instead_of_dna' do
      expect(Complement.of_dna('U')).to eq ''
    end

    it 'tests_correctly_handles_completely_invalid_dna_input' do
      expect(Complement.of_dna('XXX')).to eq ''
    end

    it 'tests_correctly_handles_partially_invalid_dna_input' do
      expect(Complement.of_dna('ACGTXXXCTTAA')).to eq ''
    end
  end
end
