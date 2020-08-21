FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithAPS-NETUdemy/db/migrations/ /home/database/
COPY ./RestWithAPS-NETUdemy/db/dataset/ /home/database/
COPY ./RestWithAPS-NETUdemy/ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh