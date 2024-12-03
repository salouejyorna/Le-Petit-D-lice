package com.example.demo;

import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
@Path("/payment-service")
public class Service {

    @Autowired
    private PaymentService paymentService;

    @POST
    @Path("/payments")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response createPayment(Payment paymentRequest) {

    	Payment payment = paymentService.processPayment(
                paymentRequest.getOrderId(),
                paymentRequest.getAmount(),
                paymentRequest.getPaymentMethod()
        );
        return Response.ok(payment).build();
    }

    @GET
    @Path("/payments")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getAllPayments() {
        List<Payment> payments = paymentService.getAllPayments();
        return Response.ok(payments).build();
    }

    @GET
    @Path("/payments/{paymentId}")
    @Produces(MediaType.APPLICATION_JSON)
    public Response getPaymentById(@PathParam("paymentId") Long paymentId) {
        Payment payment = paymentService.getPaymentById(paymentId);
        if (payment != null) {
            return Response.ok(payment).build();
        } else {
            return Response.status(Response.Status.NOT_FOUND).build();
        }
    }
}
