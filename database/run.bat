docker run --name docker_postgres -e POSTGRES_PASSWORD=123456 --rm -P --publish 192.168.99.100:5432:5432 -d --volumes-from PostgresData postgres