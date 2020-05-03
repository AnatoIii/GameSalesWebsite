import yaml

from game_sales_bot import GameSalesBot

with open("../config/config.yml", 'r') as ymlfile:
    cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)

bot = GameSalesBot(cfg['bot']['token'])
bot.start()
bot.stop()
