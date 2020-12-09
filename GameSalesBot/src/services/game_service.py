import requests
import yaml

from models.game_entry import GameEntry


class GameService:
    def __init__(self):
        with open("../config/config.yml", 'r') as ymlfile:
            self._cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)
        self._api_url = self._cfg['gateway-api']

    def get_best_games(self, count):
        games_json = requests.get("%s/gamesprices/best/%d" % (self._api_url, count)).json()
        return list(map(self._map_game, games_json))

    def _map_game(self, json):
        platforms_json = json['Platforms']
        platforms = map(lambda el: el["Id"], platforms_json)
        return GameEntry(
            name=json['Name'],
            desc=json['Description'].replace('<br>', '\n').replace('<br />', '\n'),
            image=json['Image'],
            price=json['BestPrice'],
            platforms=platforms
        )
