package com.gamesales.nintendoparser.service.impl;

import com.gamesales.nintendoparser.model.Game;
import com.gamesales.nintendoparser.service.GameParserService;
import org.jsoup.nodes.Document;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

@Service
public class NintendoStoreParserService implements GameParserService {
    @Override
    public Collection<Game> parseDocument(Document document) {
        List<Game> gameList = new ArrayList<>();
        gameList.add(new Game("Super Mario Party", "$60.00"));
        return gameList;
    }
}
