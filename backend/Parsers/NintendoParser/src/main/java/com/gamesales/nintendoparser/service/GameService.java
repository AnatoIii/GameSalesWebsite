package com.gamesales.nintendoparser.service;

import com.gamesales.nintendoparser.model.Game;

import java.util.Collection;

public interface GameService {
    Collection<Game> getGameInfosByName(String name);
}
