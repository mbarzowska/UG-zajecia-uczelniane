json.extract! game, :id, :name, :publisher, :min_players, :max_players, :release_date, :price #, :created_at, :updated_at
json.url game_url(game, format: :json)
