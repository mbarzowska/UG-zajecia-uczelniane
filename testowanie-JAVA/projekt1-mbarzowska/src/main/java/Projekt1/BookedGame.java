package Projekt1;

import java.time.LocalDate;

public class BookedGame {
    private Game Game;
    private LocalDate Date;
    private int Time;
    private String BookingID;

    public BookedGame(Game game, LocalDate date, int time, String bookingID) {
        this.Game = game;
        this.Date = date;
        this.Time = time;
        this.BookingID = bookingID;
    }

    // GETTERS, SETTERS deleted due to not using
    public Game getGame() {
        return Game;
    }

    public LocalDate  getDate() {
        return Date;
    }

    public int getTime() {
        return Time;
    }

    public String getBookingID() {
        return BookingID;
    }
}
