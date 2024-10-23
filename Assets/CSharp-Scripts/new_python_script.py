import UnityEngine
import asyncio
import websockets
import json
from gdx import gdx
import random

#Use this script with C# script UnityWebocketServer.cs
# gdx = gdx.gdx()
# gdx.open(connection='ble', device_to_open='GDX-RB 0K501575')
# gdx.select_sensors([1])
# gdx.start(100)  # Adjust the rate to 1000ms (1 second)


async def send_data():
    uri = "ws://localhost:8000/data"
    async with websockets.connect(uri) as websocket:
        while True:
            # measurements = gdx.read()
            measurements = random.randint(0,75)
            if measurements is not None:
                data = {"value": measurements}
                try: 
                    await websocket.send(json.dumps(data))
                    print(f"Send data: {data}")
                except websockets.ConnectionClosed:
                    print("Connection Closed")
                    break 
            else:
                print("No Measurements available")
            await asyncio.sleep(1)

async def main():
    await send_data()


asyncio.run(main())

# gdx.stop()
# gdx.close()
