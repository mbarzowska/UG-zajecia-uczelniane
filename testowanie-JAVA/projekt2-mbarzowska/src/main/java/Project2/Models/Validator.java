package Project2.Models;

import Project2.Models.Interfaces.Validation;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class Validator implements Validation {
    public boolean validateUser(String username, String password) {
        if (username == null || username.isEmpty() || password == null || password.isEmpty()) {
            return false;
        }
        return true;
    }

    public void validateGameName(String gameName) {
        if (gameName == null || gameName.isEmpty()) {
            throw new IllegalArgumentException("Wrong input!\nGame name is an empty string!");
        }
    }

    public LocalDate validateBookingDate(Game game, String sDate) {
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy");
        LocalDate date = LocalDate.parse(sDate, formatter);

        if (transformDaysToWords(game.getDays()).contains(date.getDayOfWeek().toString())) {
            throw new UnsupportedOperationException("You can't book a game for a date it's not available to book, sorry!");
        }
        return date;
    }

    public int validateBookingTime(Game game, String sTime) {
        int time;

        try {
            time = Integer.parseInt(sTime);
        } catch (Exception ex) {
            throw new UnsupportedOperationException("Invalid data! Please make sure given time is a number!");
        }

        if (time < 0 || time > 25) {
            throw new UnsupportedOperationException("You gave invalid number corresponding to time, sorry!");
        }

        if (time < game.getAvailableHoursStart() || time >= game.getAvailableHoursEnd()) {
            throw new UnsupportedOperationException("You can't book a game for a time period when we're closed, sorry!");
        }

        if (time >= game.getAvailableHoursStart() && time < game.getAvailableHoursEnd()) {
            if (time >= game.getUnavailableHoursStart() && time < game.getUnavailableHoursEnd()) {
                throw new UnsupportedOperationException("You can't book a game for a time period when it's not available to book, sorry!");
            }
        }
        return time;
    }

    public String transformDaysToWords(boolean[] table) {
        String[] daysInWords = {"MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY", "SUNDAY"};
        StringBuilder wordedDaysBuilder = new StringBuilder();
        for (int i = 0; i < table.length; i++) {
            if (table[i] == true) {
                wordedDaysBuilder.append(daysInWords[i] + "; ");
            } else {
                continue;
            }
        }
        String wordedDays = wordedDaysBuilder.toString();
        return wordedDays;
    }
}


