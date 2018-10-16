import MySQLdb

def pew():
	conn=MySQLdb.connect(host="localhost", user="root", passwd="13245", db="test")
	cursor = conn.cursor()

	cursor.execute("SELECT * FROM users")

	row = cursor.fetchone()
	return row

	conn.close()
	
print(pew())