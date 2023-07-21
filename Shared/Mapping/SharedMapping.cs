﻿using Mapster;
using SharedCode.Extensions;

namespace SharedCode.Mapping
{
    public static class SharedMapping
    {
        public static TypeAdapterConfig AddSharedMapping(this TypeAdapterConfig typeAdapterConfig)
        {
            typeAdapterConfig
                .AddSharedMappingAction()
                .AddSharedMappingAddress()
                .AddSharedMappingCategory()
                .AddSharedMappingContact()
                .AddSharedMappingDog()
                .AddSharedMappingLatLng()
                .AddSharedMappingNote()
                .AddSharedMappingRace()
                .AddSharedMappingRacer()
                .AddSharedMappingTerm()
                .AddSharedMappingUserProfile()
                .AddSharedMappingEntry()
                .AddSharedMappingActionRights()
                .AddSharedMappingPayment()
                .AddSharedMappingMerchandizeItem()
                .AddSharedMappingRequestedPayments();

            typeAdapterConfig.NewConfig<Google.Type.Interval, Google.Type.Interval>();
            typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Timestamp, Google.Protobuf.WellKnownTypes.Timestamp>();
            typeAdapterConfig.NewConfig<Google.Type.DateTime, Google.Type.DateTime>();
            typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Duration, Google.Protobuf.WellKnownTypes.Duration>();
            typeAdapterConfig.NewConfig<Google.Type.TimeZone, Google.Type.TimeZone>();
            typeAdapterConfig.NewConfig<Google.Type.LatLng, Google.Type.LatLng>();

            typeAdapterConfig.NewConfig<DateTimeOffset?, Google.Type.DateTime>()
                .MapWith(s => s != null ? s.ToGoogleDateTime() : null);
            
            typeAdapterConfig.NewConfig<Google.Type.DateTime, DateTimeOffset?>()
                .MapWith(s => s != null ? s.ToDateTimeOffset() : null);

            return typeAdapterConfig;
        }
    }
}
