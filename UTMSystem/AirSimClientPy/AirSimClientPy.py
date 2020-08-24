import airsim
import pprint
import time
import pymongo

client = airsim.VehicleClient()
client.confirmConnection()

myclient = pymongo.MongoClient("mongodb://localhost:27017/")
mydb = myclient["Test"]
mycol = mydb["Objects"]

spawnedObjects = []

def spawnObject(name, sizeX, sizeY, sizeZ, topleft):
    p = airsim.Pose()
    p.position = airsim.Vector3r(topleft[0], topleft[1], (topleft[2]-1.5)*-1)
    s = airsim.Vector3r(sizeX, sizeY, sizeZ)
    n = "SimObject_" + str(name)
    client.simSpawnObject(n, "mycube", p, s)
    spawnedObjects.append(name)
    print("Spawned: ", name)

def updateObject(name, topleft):
    n = "SimObject_" + str(name)
    p = airsim.Pose()
    p.position = airsim.Vector3r(topleft[0], topleft[1], (topleft[2]-1.5)*-1)
    client.simSetObjectPose(n, p)
    print("Updated: ", name)

def processObject(object, cells):
    if object["_id"] == 0:
        return 

    print("Processing: ", object["_id"], object["SizeX"], object["SizeY"], object["SizeZ"], cells[0], sep='\t')

    if object["_id"] not in spawnedObjects:
        spawnObject(object["_id"], object["SizeX"], object["SizeY"], object["SizeZ"], cells[0])
    else:
        updateObject(object["_id"], cells[0])

def getObjects():
    for x in mycol.find():
        processObject(x["SimObject"], x["CellsList"])

while(1):
    getObjects()
    time.sleep(0.05)