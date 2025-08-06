package com.example.springdemo.controller;

import org.springframework.boot.web.servlet.error.ErrorController;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import jakarta.servlet.http.HttpServletRequest;
import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.Map;

@RestController
public class CustomErrorController implements ErrorController {

    private static final String ERROR_PATH = "/error";

    @RequestMapping(ERROR_PATH)
    public ResponseEntity<Map<String, Object>> handleError(HttpServletRequest request) {
        Object status = request.getAttribute("jakarta.servlet.error.status_code");
        Object errorMessage = request.getAttribute("jakarta.servlet.error.message");
        Object requestUri = request.getAttribute("jakarta.servlet.error.request_uri");

        Map<String, Object> errorResponse = new HashMap<>();
        errorResponse.put("timestamp", LocalDateTime.now().toString());

        if (status != null) {
            int statusCode = Integer.parseInt(status.toString());
            HttpStatus httpStatus = HttpStatus.valueOf(statusCode);
            
            errorResponse.put("status", statusCode);
            errorResponse.put("error", httpStatus.getReasonPhrase());
            
            if (statusCode == 404) {
                errorResponse.put("message", "The requested resource was not found");
            } else if (statusCode == 405) {
                errorResponse.put("message", "Method not allowed for this endpoint");
            } else if (statusCode == 400) {
                errorResponse.put("message", "Bad request - please check your request format");
            } else if (statusCode >= 500) {
                errorResponse.put("message", "Internal server error occurred");
            } else {
                errorResponse.put("message", errorMessage != null ? errorMessage.toString() : "An error occurred");
            }
            
            if (requestUri != null) {
                errorResponse.put("path", requestUri.toString());
            }
            
            return new ResponseEntity<>(errorResponse, httpStatus);
        }

        // Default error response
        errorResponse.put("status", 500);
        errorResponse.put("error", "Internal Server Error");
        errorResponse.put("message", "An unexpected error occurred");
        
        return new ResponseEntity<>(errorResponse, HttpStatus.INTERNAL_SERVER_ERROR);
    }
}
