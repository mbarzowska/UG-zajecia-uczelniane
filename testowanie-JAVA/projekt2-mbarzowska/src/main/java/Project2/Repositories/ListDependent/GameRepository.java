package Project2.Repositories.ListDependent;

import Project2.Models.Game;
import Project2.Repositories.Interfaces.GameInfoHandling;

import java.util.ArrayList;
import java.util.List;

public class GameRepository implements GameInfoHandling {

    private List<Game> gamesList = new ArrayList<Game>();

    @Override
    public List getGames() {
        return gamesList;
    }

    @Override
    public Game getGame(String gameName) {
        return gamesList.stream().filter(x -> x.getName().equals(gameName)).findFirst().orElse(null);
    }

    @Override
    public void addGame(Game game) {
        gamesList.add(game);
    }

    @Override
    public boolean gameExists(String gameName) {
        return gamesList.stream().anyMatch(x -> x.getName().equals(gameName));
    }
}
