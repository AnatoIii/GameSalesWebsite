import logging.config

import yaml

with open("../config/logging.yml", 'r') as logfile:
    config = yaml.safe_load(logfile.read())
logging.config.dictConfig(config)
logger = logging.getLogger('bot_logger')


# Wrapping for catch exceptions
def exceptionHandle(func):
    def handle(*args, **kwargs):
        try:
            func(*args, **kwargs)
        except Exception as e:
            logger.error("Exception: %s" % e, exc_info=True)

    return handle
