# Use the official Nginx image as the base image
FROM nginx:latest

# Set the working directory inside the container
WORKDIR /etc/nginx

# Copy the nginx.conf file from the host to the container
COPY ./nginx.conf /etc/nginx/nginx.conf

# Expose port 80 for the Nginx web server
EXPOSE 80

# Define the command to start Nginx when the container starts
CMD ["nginx", "-g", "daemon off;"]
