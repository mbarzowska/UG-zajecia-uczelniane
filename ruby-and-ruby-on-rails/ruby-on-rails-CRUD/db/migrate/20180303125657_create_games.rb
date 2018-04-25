class CreateGames < ActiveRecord::Migration[5.2]
  def change
    create_table :games do |t|
      t.string :name
      t.string :publisher
      t.integer :min_players
      t.integer :max_players
      t.date :release_date
      t.decimal :price

      t.timestamps
    end
  end
end
