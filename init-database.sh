wait_time=15s
sleep $wait_time
echo sql server

for entry in "schemas/*.sql"
do
  echo executing $entry
  /opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $SA_PASSWORD -i $entry
done