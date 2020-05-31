package com.gamesales.nintendoparser.service;

import com.gamesales.nintendoparser.model.Game;
import org.jsoup.nodes.Document;

import java.util.Collection;

public interface GameParserService {
    Collection<Game> parseDocument(Document document);
}
