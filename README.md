# BattleArena
Использовалася такой конфиг докер контейнера для БД

`db-feature:
  image: postgres
  restart: always
  ports:
    - "5432:5432"
  environment:
    POSTGRES_USER: <bd_user>
    POSTGRES_DB: <db_name>
    POSTGRES_PASSWORD: <db_password>
  volumes:
    - ./feature_data:/var/lib/postgresql/data`  

Собрать можно через **docker-compose up -d** 
