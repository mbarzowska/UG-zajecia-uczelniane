class Game < ApplicationRecord
   validates :name, :length => { :in => 2..30 }, presence: true
   validates :publisher, :length => { :in => 2..30 }, :format => { :with => /\A[a-zA-Z0-9_ ]+\z/, :message => " contains forbidden special characters, eg.: !@#%^&*()+=-][}{;''}]}" } , presence: true
   validates :min_players, numericality: { greater_than_or_equal_to: 2 }, presence: true
   validates :max_players, numericality: { greater_than_or_equal_to: 2 }, presence: true
   validates :price, format: { with: /\A\d+(?:\.\d{0,2})?\z/ }, numericality: { greater_than_or_equal_to: 0 } , presence: true
   validate :vplayers
   private

   def vplayers
    if (self.min_players != nil && self.max_players != nil)
      if self.min_players > self.max_players
         errors.add("Minimum players", "requirement must have a value greater than maximum players requirement")
      end
    end
  end

  def self.search(search)
  where("name LIKE ?", "%#{search}%") # ILIKE on postgreSQL and Heroku, LIKE on local
  end
end
