import yaml

from game_sales_bot import GameSalesBot

# Load config
with open("../config/config.yml", 'r') as ymlfile:
    cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)

# Initialize and run bot
bot = GameSalesBot(cfg['bot']['token'])
bot.start()

# Will stop when press Ctrl+Z or Ctrl+C
bot.stop()
