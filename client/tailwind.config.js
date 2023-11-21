export default {
  content: ['./index.html', './src/**/*.{vue.js,ts,jsx,tsx}'],
  theme: { // Define custom colors for theme here
    extend: { 
      colors: {
        "primary-color": "#EBF2FF",
        "secondary-color": "#2D68D9",
      }
    },
    fontFamily: {
      Roboto: ["Roboto, sans-serif"],
    },
    container: {
      padding: "2rem",
      center: true,
    },
    screens: {
      sm: "640px",
      md: "786px"
    },
  },
  plugins: [],
}

