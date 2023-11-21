import React, { useState, useEffect, useRef } from 'react';
import { StatusBar } from 'expo-status-bar';
import { Text, View, Switch, Image, TouchableOpacity, Animated, PanResponder, Button } from 'react-native';

import styles from './Styles.js';

export default function App() {

  //Toggle var
  const [isCameraMode, setIsCameraMode] = useState(false);
  const toggleSwitch = () => setIsCameraMode(previousState => !previousState);

  //Status vars
  const [dy, setDy] = useState(0);
  const [dx, setDx] = useState(0);
  const [dir, setDir] = useState("NORTH");

    // Define the initial state for Pacman's position
    const [gunnarPosition, setGunnarPosition] = useState({ x: 15, y: 0, deg: '0deg'});

    // Function to update the position based on grid coordinates
    const moveGunnar = (gridX, gridY, dir) => {
      // Convert grid coordinates to pixel position
      const pixelX = gridX * 90 + 15;
      const pixelY = gridY * 52;

      var direction = '0deg';
      
      if(dir == "NORTH")
        direction = '-90deg';
      else if(dir == "WEST")
        direction = '-180deg';
      else if(dir == "SOUTH")
        direction = '-270deg';
      else if(dir == "EAST")
        direction = '0deg';

      // Update state with the new pixel position
      setGunnarPosition({ x: pixelX, y: pixelY, deg: direction });
    };
  //Joystick movement
  const pan = useRef(new Animated.ValueXY()).current;
  const boundary = 40; 

  const panResponder = useRef(
    PanResponder.create({
      onStartShouldSetPanResponder: () => true,
      onPanResponderGrant: () => {
        pan.setOffset({
          x: pan.x._value,
          y: pan.y._value,
        });
      },
      onPanResponderMove: Animated.event(
        [null, { dx: pan.x, dy: pan.y }],
        {
          listener: (event, gestureState) => {
            // Limit the movement within the joystickCircle
            const distance = Math.sqrt(gestureState.dx ** 2 + gestureState.dy ** 2);

            //Update power x
            if(gestureState.dx >= 0 && gestureState.dx <= 12)
            setDx(0)
            else if (gestureState.dx >= 21 && gestureState.dx <= 30)
              setDx(0.5)
            else if (gestureState.dx >= 30 && gestureState.dx <= 40)
              setDx(1)
            else if(gestureState.dx <= 0 && gestureState.dx >= -20)
              setDx(0)
            else if (gestureState.dx <= -21 && gestureState.dx >= -30)
              setDx(-0.5)
            else if (gestureState.dx <= -30 && gestureState.dx >= -40)
              setDx(-1)
            
            //Update power y
            if(gestureState.dy >= 0 && gestureState.dy <= 20)
              setDy(0)
            else if (gestureState.dy >= 21 && gestureState.dy <= 30)
              setDy(-0.5)
            else if (gestureState.dy >= 30 && gestureState.dy <= 40)
              setDy(-1)
            else if(gestureState.dy <= 0 && gestureState.dy >= -20)
              setDy(0)
            else if (gestureState.dy <= -21 && gestureState.dy >= -30)
              setDy(0.5)
            else if (gestureState.dy <= -30 && gestureState.dy >= -40)
              setDy(1)


            if (distance > boundary) {
              // Calculate the maximum x and y within the boundary
              const clampedX = boundary * (gestureState.dx / distance);
              const clampedY = boundary * (gestureState.dy / distance);
              pan.x.setValue(clampedX);
              pan.y.setValue(clampedY);
            } else {
              pan.x.setValue(gestureState.dx);
              pan.y.setValue(gestureState.dy);

              
            }
          },
          useNativeDriver: false
        }
      ),
      onPanResponderRelease: () => {
        setDx(0)
        setDy(0)
        Animated.spring(pan, {
          toValue: { x: 0, y: 0 },
          friction: 5,
          useNativeDriver: false,
        }).start();
      },
    })
  ).current;


  return (
    <View style={styles.container}>

      <StatusBar style="auto" />

      {/* Joystick code*/}
      <View style={styles.joystickContainer}> 
        <View style={styles.joystickRow}>
          <Image style={styles.arrow} source={require('./assets/arrowUp.png')}/>
        </View>

        <View style={styles.joystickRow}>

        <View style={styles.joystickCell}>
          <Image style={styles.arrow} source={require('./assets/arrowLeft.png')}/>
        </View>

        <View style={styles.joystickCell}>
          <View style={styles.joystickCircle}> 
          <Animated.View
            style={[
              styles.joystickCircle2,
              { transform: pan.getTranslateTransform()},
            ]}
            {...panResponder.panHandlers}
          ></Animated.View>
          </View>
        </View>

        <View style={styles.joystickCell}>
          <Image style={styles.arrow} source={require('./assets/arrowRight.png')}/>
        </View>

        </View>

        <View style={styles.joystickRow}>
          <Image style={styles.arrow} source={require('./assets/arrowDown.png')}/>
        </View>
      </View>

      {/* Status code*/}
      <View style={styles.statusContainer}>
        <Text>Dy: {dy}</Text>
        <Text>Dx: {dx}</Text>
        <Text>Direction: {dir}</Text>
        <Button
          onPress={() => moveGunnar(2, 2, "NORTH")}
          title="Move"
          color="#841584"
        />
      </View>

      {/* Map & Camera code*/}
      <View style={styles.showContainer}>

        {/* Toggle code*/}
        <View style={styles.toggleContainer}>
        <Text style={styles.item}>Map</Text>
          <Switch
            trackColor={{false: '#767577', true: '#81b0ff'}}
            thumbColor={isCameraMode ? '#f5dd4b' : '#f4f3f4'}
            ios_backgroundColor="#3e3e3e"
            onValueChange={toggleSwitch}
            value={isCameraMode}
            style={styles.item}
          />
          <Text style={styles.item}>Camera</Text>
        </View>

        {/* Map code */}
        { !isCameraMode && <View style={styles.mapContainer}>
          <View style={styles.mapRow}>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
          </View>
          <View style={styles.mapRow}>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
          </View>
          <View style={styles.mapRow}>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
          </View>
          <View style={styles.mapRow}>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
          </View>
          <View style={styles.mapRow}>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
            <View style={styles.mapCell}> 
              <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/> 
            </View>
          </View>

          <Image style={[styles.pac, { left: gunnarPosition.x, bottom: gunnarPosition.y, transform: [{ rotate: gunnarPosition.deg}] }]} source={require('./assets/pac.png')}/> 
          
        </View>
        }

        {/* Camera code */}
        { isCameraMode && 
        <View style={styles.mapContainer}>
          <Text>Camera Here!</Text>
        </View>
        }
      
      </View>

    </View>
  );
}