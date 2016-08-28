

SELECT        InboundSamples.SampleID
FROM            InboundSamples JOIN
                         Samples

						 on   CAST(InboundSamples.SampleID as varchar(20)) = Samples.SampleNumber