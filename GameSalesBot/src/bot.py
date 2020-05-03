import logging.config
import os
import sys
from threading import Thread

import yaml
from telegram import *
from telegram.ext import CommandHandler
from telegram.ext import Updater, Filters, CallbackContext

from exception_handler import exceptionHandle


class TelegramBot:
    def __init__(self, token: str):
        with open("../config/config.yml", 'r') as ymlfile:
            self._cfg = yaml.load(ymlfile, Loader=yaml.FullLoader)

        with open("../config/logging.yml", 'r') as logfile:
            config = yaml.safe_load(logfile.read())
        logging.config.dictConfig(config)
        self._logger = logging.getLogger('bot_logger')

        self._updater = Updater(token=token, use_context=True)
        self._dispatcher = self._updater.dispatcher
        self._dispatcher.add_handler(CommandHandler("start", self._start_message))
        self._dispatcher.add_handler(
            CommandHandler("restart", self._restart,
                           filters=Filters.user(user_id=self._cfg['bot']['admin-id'])))
        self._logger.info(u"Bot initialized. TOKEN: %s" % token)

    @exceptionHandle
    def start(self):
        self._updater.start_polling()
        self._logger.info(u"Bot started")
        self._updater.bot.send_message(chat_id=self._cfg['bot']['admin-id'], text="Bot started")

    def _stop_and_restart(self):
        """Gracefully stop the Updater and replace the current process with a new one"""
        self._updater.stop()
        self._logger.info(u"Bot is restarting")
        os.execl(sys.executable, sys.executable, *sys.argv)

    def _restart(self, update: Update = None):
        if update:
            update.message.reply_text('Bot is restarting')
        Thread(target=self._stop_and_restart).start()

    def _start_message(self, update: Update, context: CallbackContext):
        user = update.message.from_user
        message = update.message.text
        self._logger.debug(
            "Text: %s. From: %s %s, @%s, %d" %
            (message, user.first_name, user.last_name, user.username, user.id))
        context.bot.sendSticker(chat_id=update.effective_chat.id, sticker=self._cfg['bot']['start-sticker-id'])

    def stop(self):
        self._updater.idle()
        self._logger.info(u"Bot stopped")
        self._updater.bot.send_message(chat_id=self._cfg['bot']['admin-id'], text="Bot stopped")
