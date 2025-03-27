# VR Interaction Fitts' Law Experiment – Unity Project Manual

## Project Overview

This Unity project was created for the dissertation "The Impact of Motion on VR Interactions". It presents a simple VR shooting task where users interact with targets of varying size and distance, using a Meta Quest 3 headset.

## Requirements

- **Unity Version:** 2023.1.0f1 or newer (tested on 6000.0.28f1)
- **XR Plugin:** Unity XR Interaction Toolkit
- **VR Headset:** Meta Quest 3 (tested via Link with PC)
- **YawVR SDK** (integration present but not used in final test)

## How to Run the Project

1. Clone or unzip the project.
2. Open the project using **Unity Hub**.
3. Ensure the following packages are installed via Package Manager:
   - XR Interaction Toolkit
   - Input System
4. Open the scene: `Assets/Scenes/CoreVRScene.unity`
5. Ensure Meta Quest Link is active and headset is connected.
6. Enter Play Mode.
7. Press the trigger to shoot targets.

## Input Bindings

- **Right Trigger**: Shoot
- **Button A / X**: Toggle Chair Mode (if implemented)
- Other buttons unused.

## Key Scripts

- `GunController.cs`: Handles input and shooting.
- `Target.cs`: Target behavior and hit feedback.
- `TargetSpawner.cs`: Spawns targets at varying distances and sizes.
- `YawMotionManager.cs`: Optional YawVR integration logic.

## Output

- Logged data is saved to a `.csv` file on the desktop.
- Columns: Time, Target Distance.

## Notes

- Experiment was tested in **static (non-motion) mode** due to hardware access limitations.
- Code is modular and designed to support motion profiles if integrated in future.

