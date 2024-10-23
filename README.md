# RespirationProject

## Purpose
This project aims to train people to take deep breaths by utilizing visualization as feedback. 

## How it works?
**Unity Application** - Initialised as WebSocket Server. Receives real-time data from connected clients and generates visualizations. 

**Python Script** - Initialised as WebSocket Client. Uses the GDX module to read data from the GoDirect Respiration Sensor. 
Sends data read from the sensor to the WebSocket server.

## Device needed: 
**HeadMounted Display** - Meta Quest 3

**GoDirect Respiration Belt**

**Laptop**

## Software Requirements
- websocket-sharp
- gdx module from GDX
- Unity v2022.3.35f1
