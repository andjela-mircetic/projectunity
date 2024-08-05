//
//  APIWrapper.h
//  APIIntegration
//
//  Created by Andjela Mircetic on 3.8.24..
//

#ifndef APIWrapper_h
#define APIWrapper_h

#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C" {
#endif

void initializeAPI();
void generateIntegers(int n, int min, int max, void (*completion)(const char*));
void generateIntegerSequences(int n, int length, int min, int max, void (*completion)(const char*));
void generateDecimalFractions(int n, int decimalPlaces, void (*completion)(const char*));
void generateGaussians(int n, double mean, double standardDeviation, int significantDigits, void (*completion)(const char*));
void generateStrings(int n, int length, NSString *characters, void (*completion)(const char*));
void generateUUIDs(int n, void (*completion)(const char*));
void generateBlobs(int n, int size, NSString *format, void (*completion)(const char*));
void verifySignature(NSString *randomData, NSString *signature, void (*completion)(bool));
void getUsage(void (*completion)(const char*));

#ifdef __cplusplus
}
#endif


#endif /* APIWrapper_h */
