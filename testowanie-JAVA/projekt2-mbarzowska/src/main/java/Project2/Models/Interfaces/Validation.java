package Project2.Models.Interfaces;

import Project2.Models.Game;

import java.time.LocalDate;

public interface Validation {
    boolean validateUser(String username, String password);
    int validateBookingTime(Game game, String sTime);
    LocalDate validateBookingDate(Game game, String sDate);
    String transformDaysToWords(boolean[] table);
    void validateGameName(String gameName);
}
