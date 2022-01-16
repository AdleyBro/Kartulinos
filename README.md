# Kartulinos
Arcade kart racing game, inspired mostly in Mario Kart. It is meant to be a free, open-source game, so that everyone can contribute to the code. Also it is meant to give the possibility of creating addons for the game.

I don't have experience working in this kind of projects (first game, too), but I'm willing to learn and accept any help you give. Also, I don't know how to create 3D models.

This game is made in Unity and the graphics should be low-poly, similar to the NDS Mario Kart. The idea is to make it "simple" and funny to play, especially with the intention to facilitate the addition of mods or addons.

The main priorities right now are:
- Make the game playable online (with users setting up dedicated servers, for lack of a better solution)
- Build a way to easily create mods or, better said, addons.

Done things:
- The kart's physics are almost done. The drifting needs to be polished tho, as well as some other properties like acceleration and steering (mostly tweaking values).
- The kart has some kind of State machine, which also handles the power ups. It most likely can be improved, but right now it facilitates fairly well the addition of new states and power ups. The current states are: OnFloor, OnAir, Drifting, Injured. The current power ups are: Boost. You can also throw bananas and shells.
- There is only one circuit, the Mario Circuit from the DS (without moving entities). It has a checkpoint system, and the game has a lap counter working. Anyway, we can't keep this circuit because the assets are from the original game.
