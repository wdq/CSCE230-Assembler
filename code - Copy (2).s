lw r2, 20(r0) // load the pushbuttons
lw r3, 21(r0) // load the green LEDs
addi r5, r0, 4 // will be used in bitmasking
addi r5, r0, 12 // something to displaly if pushed
LOOP: lw r4, 0(r2) // load the status of pushbuttons
sw r5, 0(r3) // something to display if not pushed
cmp r5, r4 // want to know if 3 is pushed
beq LEDS
br LOOP
LEDS: sw r6, 0(r3)
lw r4, 0(r2) // load the status of the push buttons
cmp r5, r4 // want to know if button 3 is pushed
beq LEDS
br LOOP