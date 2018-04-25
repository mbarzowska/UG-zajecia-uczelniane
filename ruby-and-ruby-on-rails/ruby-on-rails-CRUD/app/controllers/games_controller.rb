class GamesController < ApplicationController
  before_action :find_game, only: [:show, :edit, :update, :destroy]
  helper_method :sort_column, :sort_direction

  def index
    if params[:search]
      @games = Game.search(params[:search]).order("created_at DESC")
    else
      @games = Game.all.order("#{sort_column} #{sort_direction}")
    end
  end

  def show
  end

  def new
    @game = Game.new
  end

  def edit
  end

  def create
    @game = Game.new(game_params)

    if @game.save
      redirect_to @game, notice: "A new game was successfully added"
    else
      render 'new'
    end
  end

  def update
    if @game.update(game_params)
      redirect_to @game, notice: "A game was successfully updated"
    else
      render 'edit'
    end
  end

  def destroy
    @game.destroy
    redirect_to games_path, notice: "A game was successfully deleted"
  end

  private

  def find_game
    @game = Game.find(params[:id])
  end

  def game_params
    params.require(:game).permit(:name, :publisher, :min_players, :max_players, :release_date, :price)
  end

  def sortable_columns
    ["name", "publisher", "min_players", "max_players", "release_date", "price"]
  end

  def sort_column
    sortable_columns.include?(params[:column]) ? params[:column] : "name"
  end

  def sort_direction
    %w[asc desc].include?(params[:direction]) ? params[:direction] : "asc"
  end

end
