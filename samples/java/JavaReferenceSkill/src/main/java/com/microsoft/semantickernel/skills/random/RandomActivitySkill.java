package com.microsoft.semantickernel.skills.random;

import io.grpc.stub.StreamObserver;
import reference_skill.ActivityOuterClass.GetRandomActivityRequest;


import reference_skill.ActivityOuterClass.GetRandomActivityResponse;
<<<<<<< div
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< main
=======
=======
<<<<<<< div
>>>>>>> main
=======
>>>>>>> origin/main
>>>>>>> head
import reference_skill.ActivityOuterClass;
>>>>>>> origin/main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
import reference_skill.ActivityOuterClass;
>>>>>>> origin/main
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
import reference_skill.ActivityOuterClass;
>>>>>>> origin/main
>>>>>>> Stashed changes
import reference_skill.RandomActivitySkillGrpc;

import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.util.concurrent.CompletableFuture;
import java.util.logging.Logger;

public class RandomActivitySkill extends RandomActivitySkillGrpc.RandomActivitySkillImplBase {

    @Override
    protected Object clone() throws CloneNotSupportedException {
        // // The comment `TODO Auto-generated method stub` is a placeholder comment often
        // automatically generated by IDEs to indicate that the method implementation is incomplete
        // and needs to be filled in by the developer. It serves as a reminder to the developer that
        // they need to add the actual implementation logic for the method.
        // TODO Auto-generated method stub
        return super.clone();
    }

    @Override
    public boolean equals(Object obj) {
        // TODO Auto-generated method stub
        return super.equals(obj);
    }

    // Removed deprecated finalize method

    @Override
    public int hashCode() {
        // TODO Auto-generated method stub
        return super.hashCode();
    }

    // Removed duplicate getRandomActivity method

<<<<<<< div
<<<<<<< div
=======
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< main
=======
=======
>>>>>>> origin/main
=======
<<<<<<< main
=======
>>>>>>> Stashed changes
=======
<<<<<<< main
=======
<<<<<<< div
=======
>>>>>>> main
=======
>>>>>>> Stashed changes
>>>>>>> head
    public static final String API_ACTIVITY_URL = "https://www.boredapi.com/api/activity";

    public static String getApiActivityUrl() {
        return API_ACTIVITY_URL;
    }

<<<<<<< div
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
>>>>>>> head
>>>>>>> origin/main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
<<<<<<< div
>>>>>>> main
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
>>>>>>> origin/main
>>>>>>> head
    public static final String API_ACTIVITY_URL = "https://www.boredapi.com/api/activity";

    public static String getApiActivityUrl() {
        return API_ACTIVITY_URL;
    }

    /**
     * <pre>
     * GetRandomActivity is an RPC method that retrieves a random activity from an API.
     * </pre>
     *
     * @param request
     * @param responseObserver
     */
    @Override
    public void getRandomActivity(GetRandomActivityRequest request, StreamObserver<GetRandomActivityResponse> responseObserver) {
<<<<<<< div
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< main
=======
=======
<<<<<<< div
>>>>>>> main
=======
>>>>>>> origin/main
>>>>>>> head
    public void getRandomActivity(ActivityOuterClass.GetRandomActivityRequest request, StreamObserver<ActivityOuterClass.GetRandomActivityResponse> responseObserver) {
>>>>>>> origin/main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes
<<<<<<< main
=======
    public void getRandomActivity(ActivityOuterClass.GetRandomActivityRequest request, StreamObserver<ActivityOuterClass.GetRandomActivityResponse> responseObserver) {
>>>>>>> origin/main
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        Logger logger =  java.util.logging.Logger.getLogger(this.getClass().getName());
        HttpClient httpClient = HttpClient.newHttpClient();
        HttpRequest httpRequest = HttpRequest.newBuilder()
                .uri(URI.create(API_ACTIVITY_URL))
                .build();
        try {
            CompletableFuture<HttpResponse<String>> response = httpClient.sendAsync(httpRequest, HttpResponse.BodyHandlers.ofString());
            logger.info("Response: " + response.get().body());
            responseObserver.onNext(GetRandomActivityResponse.newBuilder().setActivity(response.get().body()).build());
<<<<<<< div
<<<<<<< div
=======
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< head
>>>>>>> head
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
<<<<<<< main
=======
=======
<<<<<<< div
>>>>>>> main
=======
>>>>>>> origin/main
>>>>>>> head
            responseObserver.onNext(ActivityOuterClass.GetRandomActivityResponse.newBuilder().setActivity(response.get().body()).build());
>>>>>>> origin/main
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
=======
>>>>>>> Stashed changes
<<<<<<< main
=======
            responseObserver.onNext(ActivityOuterClass.GetRandomActivityResponse.newBuilder().setActivity(response.get().body()).build());
>>>>>>> origin/main
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
            responseObserver.onCompleted();
        } catch (Exception e) {
            logger.severe("Error with request: " + e.getMessage());
        }
    }

    @Override
    public String toString() {
        return "RandomActivitySkill []";
    }
}
