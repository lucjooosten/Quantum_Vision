# Base image
FROM node:lts-alpine AS build

# Working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the application
RUN npm run build

# Stage 2: Serve the application from Nginx
FROM nginx:alpine

# Remove the default Nginx welcome page (recursively)
RUN rm -rf /usr/share/nginx/html/*

# Copy the built application from the previous stage
COPY --from=build /app/dist/quantum-vision/browser /usr/share/nginx/html

# Set the user to run Nginx
RUN chown -R nginx:nginx /usr/share/nginx/html

# Expose the port Nginx runs on
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]