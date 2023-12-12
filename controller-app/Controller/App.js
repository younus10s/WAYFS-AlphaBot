import React, { useState, useEffect, useRef } from 'react'
import { StatusBar } from 'expo-status-bar'
import { Text, View, Switch, Image, TouchableOpacity, Animated, PanResponder } from 'react-native'
import { WebView } from 'react-native-webview'

import styles from './Styles.js'

export default function App (){
  // Toggle var
  const [isCameraMode, setIsCameraMode] = useState(false)
  const toggleSwitch = () => setIsCameraMode(previousState => !previousState)
  const [webSocket, setWebSocket] = useState(null)

  useEffect(() => {
    // Function to initialize WebSocket connection
    const connectWebSocket = () => {
      const newWebSocket = new WebSocket('ws://192.168.187.236:5000')
      // const newWebSocket = new WebSocket('ws://localhost:5000');
      console.log('Trying to connect...')

      newWebSocket.onopen = () => {
        console.log('Connected to WebSocket')
        const placeString = 'PLACE,0,0,NORTH';
        const msg = {
          Title : 'placing',
          Msg : [placeString]
        }
        newWebSocket.send(JSON.stringify(msg));
        console.log('Sending: Placing,0,0,NORTH');
        moveGunnar(0, 0, 'NORTH');
      };

      newWebSocket.onmessage = (event) => {
        console.log('Message from server ');
        const message = JSON.parse(event.data);
        if (message.Title === 'status')
          moveGunnar(message.Msg[2], message.Msg[3], message.Msg[4].toUpper());
        console.log(message);
      };

      setWebSocket(newWebSocket);
    };

    connectWebSocket();

    // Cleanup function to close WebSocket connection
    return () => {
      if (webSocket) {
        webSocket.close();
        console.log('WebSocket disconnected');
      }
    };
  }, []);

  //Status vars
  const [dy, setDy] = useState(0);
  const [dx, setDx] = useState(0);
  const [dir, setDir] = useState('NORTH');

    // Define the initial state for Pacman's position
    const [gunnarPosition, setGunnarPosition] = useState({ x: 15, y: 0, deg: '0deg'});

    // Function to update the position based on grid coordinates
    const moveGunnar = (gridX, gridY, dir) => {
      // Convert grid coordinates to pixel position
      const pixelX = gridX * 90 + 15;
      const pixelY = gridY * 52;

      var direction = '0deg';
      
      if (dir === 'NORTH')
      {
        direction = '-90deg';
      }
      else if (dir === 'WEST')
      {
        direction = '-180deg';
      }
      else if (dir === 'SOUTH')
      {
        direction = '-270deg';
      }
      else if (dir === 'EAST')
      {
        direction = '0deg';
      }

      // Update state with the new pixel position
      setGunnarPosition({ x: pixelX, y: pixelY, deg: direction });
    };

  // Joystick movement
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

            if (distance > boundary) 
            {
              // Calculate the maximum x and y within the boundary
              const clampedX = boundary * (gestureState.dx / distance);
              const clampedY = boundary * (gestureState.dy / distance);
              pan.x.setValue(clampedX);
              pan.y.setValue(clampedY);
              setDx(Number(clampedX/boundary).toFixed(2));
              setDy(-1* Number(clampedY/boundary).toFixed(2));
            } 
            else 
            {
              pan.x.setValue(gestureState.dx);
              pan.y.setValue(gestureState.dy);
              setDx(Number(gestureState.dx/boundary).toFixed(2));
              setDy(-1 * Number(gestureState.dy/boundary).toFixed(2));


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

  useEffect(() => {
    // Koden här kommer att köras varje gång variabel1 eller variabel2 ändras
    if (webSocket != null)
    {
      const msg = {
        'Title': 'movement',
        'Msg': [dx.toString(), dy.toString()]
      }
      // const fullCommands = combineCommands();
      webSocket.send(JSON.stringify(msg));
      console.log('Sending:');
      console.log(msg);
    }
  }, [dx, dy]); // Dependency array

  const handleCellClick = (x, y) => {
    // Example: Move to (100, 100) and rotate 45 degrees
    const msg = {
        Title: 'gridCoor',
        Msg: [x.toString(), y.toString()]
    }

    webSocket.send(JSON.stringify(msg));
    console.log('Sending:')
    console.log('(' + x + ':' + y + ')')

  };

  function createGrid () {
    let grid = [];
    for (let row = 4; row >= 0; row--) {
        let cells = [];
        for (let col = 0; col < 5; col++) {
            cells.push(
                <TouchableOpacity  key={`cell-${row}-${col}`} style={styles.mapCell} onPress={() => handleCellClick(col, row) }>
                    <Image style={styles.mapCellImg} source={require('./assets/plus.png')}/>
                </TouchableOpacity >
            );
        }
        grid.push(<View key={`row-${row}`} style={styles.mapRow}>{cells}</View>);
    }
    return grid;
  }

  return (
    <View style={styles.container}>

      <StatusBar style='auto' />

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
      </View>

      {/* Map & Camera code*/}
      <View style={styles.showContainer}>

        {/* Toggle code*/}
        <View style={styles.toggleContainer}>
        <Text style={styles.item}>Map</Text>
          <Switch
            trackColor={{false: '#767577', true: '#81b0ff'}}
            thumbColor={isCameraMode ? '#f5dd4b' : '#f4f3f4'}
            ios_backgroundColor='#3e3e3e'
            onValueChange={toggleSwitch}
            value={isCameraMode}
            style={styles.item}
          />
          <Text style={styles.item}>Camera</Text>
        </View>

        {/* Map code */}
        { !isCameraMode && <View style={styles.mapContainer}>
          {createGrid()}

          <Image style={[styles.pac, { left: gunnarPosition.x, bottom: gunnarPosition.y, transform: [{ rotate: gunnarPosition.deg}] }]} source={require('./assets/pac.png')}/> 
          
        </View>
        }

        {/* Camera code */}
        { isCameraMode && 
        <View style={styles.mapContainer}>
          <WebView
            source={{ uri: '192.168.187.236:8000' }}
            style={{ width: 500, height: 500 }}
          />
        </View>
        }
      </View>

    </View>
  );
}
