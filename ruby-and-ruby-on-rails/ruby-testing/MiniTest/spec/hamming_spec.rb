require_relative '../lib/hamming'

RSpec.describe '../lib/hamming.rb' do
  describe '#compute' do
    it 'passes when strings are empty' do
      expect(Hamming.compute('', '')).to eq 0
    end

    it 'passes when strings are identical' do
      expect(Hamming.compute('A', 'A')).to eq 0
    end

    it 'passes when strings are identical' do
      expect(Hamming.compute('GGACTGA', 'GGACTGA')).to eq 0
    end

    it 'does test_complete_distance_in_single_nucleotide_strands' do
      expect(Hamming.compute('A', 'G')).to eq 1
    end

    it 'does test_complete_distance_in_small_strands' do
      expect(Hamming.compute('AG', 'CT')).to eq 2
    end

    it 'test small distance in small strands' do
      expect(Hamming.compute('AT', 'CT')).to eq 1
    end

    it 'tests small distance' do
      expect(Hamming.compute('GGACG', 'GGTCG')).to eq 1
    end

    it 'tests small distance in long strands' do
      expect(Hamming.compute('ACCAGGG', 'ACTATGG')).to eq 2
    end

    it 'tests non unique character in first strand' do
      expect(Hamming.compute('AAG', 'AAA')).to eq 1
    end

    it 'tests non unique character in second strand' do
      expect(Hamming.compute('AAA', 'AAG')).to eq 1
    end

    it 'tests same nucleotides in different positions' do
      expect(Hamming.compute('TAG', 'GAT')).to eq 2
    end

    it 'tests large distance' do
      expect(Hamming.compute('GATACA', 'GCATAA')).to eq 4
    end

    it 'tests large distance in off by one strand' do
      expect(Hamming.compute('GGACGGATTCTG', 'AGGACGGATTCT')).to eq 9
    end

    it 'tests disallow first strand longer' do
      expect { Hamming.compute('AATG', 'AAA') }.to raise_error(ArgumentError)
    end

    it 'tests disallow second strand longer' do
      expect { Hamming.compute('ATA', 'AGTG') }.to raise_error(ArgumentError)
    end
  end
end
